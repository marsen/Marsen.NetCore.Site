using System;

namespace Marsen.Business.Logic.Entities
{    
    /// <summary>
    /// ProductEntity
    /// </summary>
    public class ProductEntity
    {        		
		/// <summary>
		/// Id
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Information
		/// </summary>
		public string Information { get; set; }

		/// <summary>
		/// Spec
		/// </summary>
		public string Spec { get; set; }

		/// <summary>
		/// Picture
		/// </summary>
		public string Picture { get; set; }


    }
}