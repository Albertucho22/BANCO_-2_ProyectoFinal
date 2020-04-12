using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class LocalTransfer
    {
         /*
         Cuenta que envia
         Cuenta que recibe
         MOnto... negativo para la que envia, positivo para la que recbe
         */
        public int Id { get; set; }


        [Required]
        public string TransmitterAccountId { get; set; }


        [Required]
        public string ReciverAccountId { get; set; }


        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal OutgoingAmount { get; private set; }


        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal IncomingAmount { get; private set; }


    }
}
