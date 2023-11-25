using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Application.Dtos.Usuarios;
using AzucareraPomalca.Core.Securities.Services;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, ISecurityService securityService, IConfiguration configuration)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _securityService = securityService;
            _configuration = configuration;
        }

        public async Task<UsuarioDto> CreateAsync(UsuarioSaveDto saveDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(saveDto);
            usuario.CreatedAt = DateTime.UtcNow;
            usuario.State = true;

            usuario.Clave = _securityService.HashPassword(saveDto.Correo, saveDto.Clave);

            await _usuarioRepository.SaveAsync(usuario);

            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> DisabledAsync(int id)
        {
            Usuario? usuario = await _usuarioRepository.FindByIdAsync(id);

            if (usuario is null) throw UsuarioNotFound(id);

            usuario.State = !usuario.State;

            await _usuarioRepository.SaveAsync(usuario);

            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<UsuarioDto> EditAsync(int id, UsuarioSaveDto saveDto)
        {
            Usuario? usuario = await _usuarioRepository.FindByIdAsync(id);

            if (usuario is null) throw UsuarioNotFound(id);

            _mapper.Map<UsuarioSaveDto, Usuario>(saveDto, usuario);

            usuario.Clave = _securityService.HashPassword(saveDto.Correo, saveDto.Clave);
            usuario.UpdatedAt = DateTime.UtcNow;

            await _usuarioRepository.SaveAsync(usuario);

            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<IReadOnlyList<UsuarioDto>> FindAllAsync()
        {
            List<Expression<Func<Usuario, object>>> includes = new List<Expression<Func<Usuario, object>>>()
            {
                t => t.Rol
            };

            var response = await _usuarioRepository.FindAllAsync(includes: includes);

            return _mapper.Map<IReadOnlyList<UsuarioDto>>(response);
        }

        public async Task<PageResponse<UsuarioDto>> FindAllPaginatedAsync(PageRequest<UsuarioFilterDto> request)
        {
            var filter = request.Filter ?? new UsuarioFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Usuario, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Correo) || x.Correo.ToUpper().Contains(filter.Correo.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Nombres) || x.Nombres.ToUpper().Contains(filter.Nombres.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.Apellidos) || x.Apellidos.ToUpper().Contains(filter.Apellidos.ToUpper()))
                && (!filter.IdRol.HasValue || x.IdRol == filter.IdRol)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Usuario, object>>> includes = new List<Expression<Func<Usuario, object>>>()
            {
                t => t.Rol
            };

            var response = await _usuarioRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<UsuarioDto>>(response);
        }

        public async Task<UsuarioDto> FindByIdAsync(int id)
        {
            Expression<Func<Usuario, bool>> predicate = x => x.Id == id;

            List<Expression<Func<Usuario, object>>> includes = new List<Expression<Func<Usuario, object>>>()
            {
                t => t.Rol
            };

            var response = await _usuarioRepository.FindByIdAsync(predicate: predicate, includes: includes);

            if (response is null) throw UsuarioNotFound(id);

            return _mapper.Map<UsuarioDto>(response);
        }

        public async Task<UsuarioSecurityDto> LoginAsync(UsuarioAuthDto userAuth)
        {
            Expression<Func<Usuario, bool>> predicate = x => x.Correo == userAuth.Correo;

            List<Expression<Func<Usuario, object>>> includes = new List<Expression<Func<Usuario, object>>>()
            {
                t => t.Rol
            };

            Usuario? usuario = await _usuarioRepository.FindFirstOrDefaultAsync(predicate: predicate, includes: includes);

            if (usuario is null) throw new NotFoundCoreException("Usuario no esta registrado en nuestro Sistema.");

            bool isCorrect = _securityService.VerifyHashedPassword(usuario.Correo, usuario.Clave, userAuth.Clave);

            if (!isCorrect) throw new NotFoundCoreException("La contraseña que ingreso no es correcta");

            if (!usuario.State) throw new NotFoundCoreException("Usuario no esta activo. Comuniquese con el adminstrador");

            UsuarioSecurityDto userSecurity = _mapper.Map<UsuarioSecurityDto>(usuario);

            string jwtSecretKey = _configuration.GetSection("Security:JwtSecrectKey").Get<string>();

            userSecurity.Security = _securityService.JwtSecurity(jwtSecretKey);

            return userSecurity;
        }

        private NotFoundCoreException UsuarioNotFound(int id)
        {
            return new NotFoundCoreException("Usuario no encontrado para el id: " + id);
        }
    }
}
