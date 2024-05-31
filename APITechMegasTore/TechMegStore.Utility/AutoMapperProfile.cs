using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Referencias
using AutoMapper;
using TechMegStore.DTO;
using TechMegStore.Models;



namespace TechMegStore.Utility
{
    public class AutoMapperProfile : Profile //Añadimos el profile del cual le pertence a Automapper
    {

        //Creamos el constructor utilizando el comando ctor y tab
        public AutoMapperProfile()
        {
            //Procedemos a crear los mapeos

            #region Role
            CreateMap<Role, RoleDTO>().ReverseMap();
            #endregion Role

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu


            #region Useer
            CreateMap<Useer, UseerDTO>().
                //Especificamos como sera la conversion hacia nuestro destino
                ForMember(destination => //destination no viene de ningun lado solo es nombre de variable
                destination.RoleDescription,
                opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name) //origin no viene de ningun lado solo es nombre de variable
                )
                .ForMember(destination =>
                destination.Active,
                opt => opt.MapFrom(origin => origin.Active == true ? 1 : 0)
                );
            CreateMap<Useer, SesionDTO>().
                //Especificamos como sera la conversion hacia nuestro destino
                ForMember(destination => //destination no viene de ningun lado solo es nombre de variable
                destination.RoleDescription,
                opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name) //origin no viene de ningun lado solo es nombre de variable
                );

            CreateMap<UseerDTO, Useer>().
                //Personalizamos las propiedades
                ForMember(destination =>
                destination.IdRoleNavigation,
                opt => opt.Ignore()
                )
                .ForMember(destination =>
                destination.Active,
                opt => opt.MapFrom(origin => origin.Active == 1 ? true : false)
                );
            #endregion Useer


            #region Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion Category



            #region Product
            CreateMap<Product, ProductDTO>()
                //Trabajamos conversiones de sus propiedades
                .ForMember(destination =>
                destination.DescriptionCategory,
                opt => opt.MapFrom(origin => origin.IdCategoryNavigation.Name)
                )
                .ForMember(destination =>
                destination.Price,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-MX")))
                )
                .ForMember(destination =>
                destination.Active,
                opt => opt.MapFrom(origin => origin.Active == true ? 1 : 0)
                );

            //Ahora lo trabajaremos de manera inversa
            CreateMap<ProductDTO, Product>()
              //Trabajamos conversiones de sus propiedades
              .ForMember(destination =>
              destination.IdCategoryNavigation,
              opt => opt.Ignore()
              )
              .ForMember(destination =>
              destination.Price,
              opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo("es-MX")))
              )
              .ForMember(destination =>
              destination.Active,
              opt => opt.MapFrom(origin => origin.Active == 1 ? true : false)
              );

            #endregion Product


            //Venta
            #region Sale
            CreateMap<Sale, SaleDTO>()
                   //Trabajamos conversiones de sus propiedades
                   //Total de la venta
                   .ForMember(destination =>
                      destination.SaleTotalText,
                      opt => opt.MapFrom(origin => Convert.ToString(origin.SaleTotal.Value, new CultureInfo("es-MX")))
                      )
                   .ForMember(destination =>
                        destination.DateRegistration,
                         opt => opt.MapFrom(origin => origin.DateRegistration.Value.ToString("dd/MM/yyyy"))
                     );

            //Ahora lo trabajaremos de manera inversa
            CreateMap<SaleDTO, Sale>()
                   //Trabajamos conversiones de sus propiedades
                   //Total de la venta
                   .ForMember(destination =>
                     destination.SaleTotal,
                     opt => opt.MapFrom(origin => Convert.ToDecimal(origin.SaleTotalText, new CultureInfo("es-MX")))
                     );


            #endregion Sale


            //Detalle de venta
            #region SaleDetail
            CreateMap<SaleDetail, SaleDetailDTO>()
                //Trabajamos conversiones de cada una de  sus propiedades
                .ForMember(destination =>
                destination.DescriptionProduct,
                opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
                )
                .ForMember(destination =>
                destination.PriceText,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-MX")))
                )
                 .ForMember(destination =>
                destination.PriceText,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-MX")))
                );

            //Ahoraa a la inversa
            CreateMap<SaleDetailDTO, SaleDetail>()
                 //Trabajamos conversiones de cada una de  sus propiedades
                 .ForMember(destination =>
                destination.Price,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.PriceText, new CultureInfo("es-MX")))
                )
                  .ForMember(destination =>
                destination.Total,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalText, new CultureInfo("es-MX")))
                );

            #endregion SaleDetail


            //Ahora se trabaja con Reportes


            #region Report
            CreateMap<SaleDetail, ReportDTO>()
                //Trabajamos conversiones de cada una de  sus propiedades
                .ForMember(destination =>
                        destination.DateRegistration,
                         opt => opt.MapFrom(origin => origin.IdSaleNavigation.DateRegistration.Value.ToString("dd/MM/yyyy"))
                     )
                .ForMember(destination =>
                        destination.DocumentNumber,
                         opt => opt.MapFrom(origin => origin.IdSaleNavigation.DocumentNumber)
                     )
                //Tipo de pago
                .ForMember(destination =>
                        destination.PaymentType,
                         opt => opt.MapFrom(origin => origin.IdSaleNavigation.PaymentType)
                     )
                //Total de venta
                .ForMember(destination =>
                        destination.SaleTotal,
                         opt => opt.MapFrom(origin => Convert.ToString(origin.IdSaleNavigation.SaleTotal.Value, new CultureInfo("es-MX")))
                     )
                //Producto
                .ForMember(destination =>
                destination.Product,
                opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
                )
                //Precio 
                .ForMember(destination =>
                        destination.Price,
                         opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-MX")))
                     )
                //Total
                .ForMember(destination =>
                        destination.Total,
                         opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-MX")))
                     );



            #endregion Report

        }
    }
}
