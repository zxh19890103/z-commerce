﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.Model
{
    public abstract class BaseEntity : BaseModel, IEntity
    {
        public int Id { get; set; }
    }
}
