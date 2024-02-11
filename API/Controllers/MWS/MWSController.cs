using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using API.DTOs;
using API.Entities.MWS;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using AutoMapper;



namespace API.Controllers.MWS
{
        [Authorize]
    public class MWSController : BaseApiController
    {

        private readonly IApplicationMWSDbContext _contextmws;
        private readonly IMWSMasterRepository _mwsMasterRepository;
        private readonly IMapper _mapper;

        public MWSController(IApplicationMWSDbContext contextmws, IMWSMasterRepository mwsMasterRepository, IMapper mapper)
        {
            _mapper = mapper;
            _mwsMasterRepository = mwsMasterRepository;
            _contextmws = contextmws;
        }
   }
}

