using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestioAPI.Entities;
using TestioAPI.Models.Taks;
using Xunit;

namespace TestioAPITest.AutomapperTest
{
    public class AutoMapperTest
    {
        internal  IMapper _mapper;
        private MapperConfiguration _mapperConfiguration;


        public AutoMapperTest()
        {
            _mapperConfiguration = new MapperConfiguration(cfg => 
            cfg.CreateMap<Tasks, TaskModel>());

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [Fact]
        [Category("AutoMapper")]
        public void Map_Should_HaveValidConfig()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }

    }
}
