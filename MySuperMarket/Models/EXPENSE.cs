//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySuperMarket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EXPENSE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXPENSE()
        {
            this.SALARY = new HashSet<SALARY>();
            this.STOCK = new HashSet<STOCK>();
        }
    
        public string EXPENSE_ID { get; set; }
        public Nullable<System.DateTime> EXPENSE_DATE { get; set; }
        public Nullable<long> MONEY { get; set; }
        public string TYPE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SALARY> SALARY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STOCK> STOCK { get; set; }
    }
}
