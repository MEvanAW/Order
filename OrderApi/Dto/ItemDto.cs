using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;

namespace OrderApi.Dto
{
    [SwaggerSchemaFilter(typeof(ItemDtoSchemaFilter))]
    public class ItemDto
    {
        /// <summary>
        /// Code of the item
        /// </summary>
        /// <example>SGA32</example>
        [Required]
        public string? ItemCode { get; set; }
        /// <summary>
        /// Description of the item
        /// </summary>
        /// <example>Samsung Galaxy A32</example>
        public string? Description { get; set; }
        /// <summary>
        /// Quantity of the item
        /// </summary>
        /// <example>1</example>
        [Required, Range(1, uint.MaxValue)]
        public uint? Quantity { get; set; }
    }

    public class ItemDtoSchemaFilter: ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Example = new OpenApiObject
            {
                ["itemCode"] = new OpenApiString("SGA32"),
                ["description"] = new OpenApiString("Samsung Galaxy A32"),
                ["quantity"] = new OpenApiInteger(1)
            };
        }
    }
}
