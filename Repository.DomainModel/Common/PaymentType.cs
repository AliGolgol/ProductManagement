﻿using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Common
{
    public class PaymentType
    {
        public int Id { get; set; }
        [DisplayName("نحوه پرداخت")]
        [Required(ErrorMessage = "نحوه پرداخت را وارد کنید")]
        public string  Name { get; set; }
        [DisplayName("توضیحات")]
        public string  Description { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
    }
}
