﻿using Application.Core;
using Application.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClusterNodeRepository : BaseRepository<ClusterNode>
    {
    }
}
