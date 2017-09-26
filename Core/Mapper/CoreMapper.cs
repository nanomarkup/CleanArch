using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Core.Mapper
{
    public static class CoreMapper
    {
        public static IMapper Mapper()
        {
            return AutoMapper.Mapper.Instance;
        }

        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EntityProfile());
                cfg.AddProfile(new GatewayProfile());
                cfg.AddProfile(new InteractorProfile());
            });
        }
    }
}
