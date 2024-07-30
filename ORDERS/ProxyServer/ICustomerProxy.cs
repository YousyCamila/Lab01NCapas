﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyServer
{
    public interface ICustomerProxy
    {
        Task<Customer> CreateAsync(Customer customer);
       Task<bool> DeleteAsync(int id);

 
    }
}
