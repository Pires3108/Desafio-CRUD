using AutoMapper;
using ClienteCRUD.Application.DTOs;
using ClienteCRUD.Core.Entities;

namespace ClienteCRUD.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeamento de Cliente para ClienteDTO
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dest => dest.EquipeNome, opt => opt.MapFrom(src => src.Equipe != null ? src.Equipe.Nome : null));

            // Mapeamento de CriarClienteDTO para Cliente
            CreateMap<CriarClienteDTO, Cliente>()
                .ForMember(dest => dest.EquipeId, opt => opt.Ignore())
                .ForMember(dest => dest.Equipe, opt => opt.Ignore())
                .ForMember(dest => dest.IsEquipeAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.IsLandTechAdmin, opt => opt.Ignore());

            // Mapeamento de AtualizarClienteDTO para Cliente
            CreateMap<AtualizarClienteDTO, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                .ForMember(dest => dest.Ativo, opt => opt.Ignore())
                .ForMember(dest => dest.CriadoPor, opt => opt.Ignore())
                .ForMember(dest => dest.AtualizadoPor, opt => opt.Ignore())
                .ForMember(dest => dest.EquipeId, opt => opt.Ignore())
                .ForMember(dest => dest.Equipe, opt => opt.Ignore())
                .ForMember(dest => dest.IsEquipeAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.IsLandTechAdmin, opt => opt.Ignore());
        }
    }
} 