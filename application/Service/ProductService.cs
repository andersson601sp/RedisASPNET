using System;
using application.Entities;
using application.Interface;
using application.Infra;
using ServiceStack.Redis;

namespace application.Service
{
    public class ProductService : IProductService
    {
        private readonly string _host = EnvConfig.RedisHost();
        public ProductService()
        {
            
        }

        public int Generate(Productor request)
        {
            try
            {

                using(var redisCliente = new RedisClient(_host))
                {
                    redisCliente.Set<Productor>(request.Id.ToString(), request);
                }
                return 200;
            }
            catch (Exception ex)
            {
                var m = ex.Message;
                return 404;
            }
        }

        public Productor Receive(int? key)
        {
            using (var redisCliente = new RedisClient(_host))
            {
                var result = redisCliente.Get<Productor>(key.ToString());
                return result;
            }
        }
    }
}
