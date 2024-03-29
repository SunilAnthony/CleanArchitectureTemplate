﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Intefaces.Settings
{
    public interface IJsonSerializerSettings
    {
        /// <summary>
        /// Settings for <see cref="Newtonsoft.Json"/>.
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; }
    }
}
