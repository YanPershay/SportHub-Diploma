﻿using AutoMapper;
using SportHub.Application.Mappers.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportHub.Application.Mappers
{
    public class SubscriptionsCountMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<SubscriptionsCountMappingProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
