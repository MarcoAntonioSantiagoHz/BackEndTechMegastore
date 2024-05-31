using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//Agregasmoas referencias
using TechMegStore.DATAS.DBContext;
using TechMegStore.DATAS.Repositories.Interfaces;
using TechMegStore.Models;


namespace TechMegStore.DATAS.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        //Declaramos los metodos implementados dentro de la interfaz IGenericRepository del cual hereda


        //Creamos una varible  de lectura hacia el contexto de la base de datos
        private readonly TechMegastoreDbContext _dbContext;

        //Creamos el constructor donde estamos recibiendo el contexto, y lo almacena en nuestra variable de arriba   private readonly DbContext _dbContext;
        public SaleRepository(TechMegastoreDbContext dbContext) : base(dbContext)
        {
            {
                _dbContext = dbContext;
            }


        }

        //Metodo para regfistrar una venta
        public  async Task<Sale> Register(Sale model)
        {
            //Creamos una varible
            Sale saleGenerated = new Sale();
            //Instanciamos la varibale
            //Si ocurre un error al insertar algun dato, se volvera a cero como estaba antes
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Restar el stock de cada producto que esta involucrado dentro de la venta
                    foreach (SaleDetail dv in model.SaleDetails)
                    {
                        Product found_product= _dbContext.Products.Where(p => p.IdProduct == dv.IdProduct).First();

                        found_product.Stock = found_product.Stock - dv.Amount;
                        _dbContext.Products.Update(found_product);
                    }

                    //Guardamos cambios de manera asincrona 
                    await _dbContext.SaveChangesAsync();


                    //Generamos un numero de documento
                    DocumentNumber correlative = _dbContext.DocumentNumbers.First(); //Devulve el primero
                    correlative.LastNumber = correlative.LastNumber + 1;
                    correlative.DateRegistration = DateTime.Now;

                    _dbContext.DocumentNumbers.Update(correlative);
                    await _dbContext.SaveChangesAsync();  //Guardamos cambios de manera asincrona 



                    //Generar el formato del numero de documento de venta ejemplo asi: 0001 0002 0003 etc.
                    int AmountOfDigits = 4; //Cantidad de digitos
                    string ceros = string.Concat(Enumerable.Repeat("0", AmountOfDigits)); //Cantidad de ceros que tendra
                    //Creamos el numero de venta
                    string numberSale = ceros + correlative.LastNumber.ToString();
                    numberSale = numberSale.Substring(numberSale.Length - AmountOfDigits, AmountOfDigits );
                    //Actualizar
                    model.DocumentNumber = numberSale;

                    //Acceder a a base de datos y agregamos de manera asincrona el modelo

                    await _dbContext.Sales.AddAsync(model);
                    await _dbContext.SaveChangesAsync(); //Guardamos cambios de manera asincrona 

                    //Llamamos a la variable ventaGenerada y le pasamos el modelo
                    saleGenerated = model;


                    //En dado caso que toda la transaccion sea correcta
                    transaction.Commit();
                }
                catch
                {
                    //Podra restablecer todo si existe un error
                    transaction.Rollback();
                    throw;
                }

                //Retornara nuestra venta generada
                return saleGenerated;
            }
        }
    }

}
