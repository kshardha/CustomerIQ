using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Migration;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Indexing;

namespace Orchard.CustomerIQ
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {

            SchemaBuilder.CreateTable("CustomerPartRecord", table => table
                 .ContentPartRecord()
                 .Column<string>("FirstName", c => c.WithLength(50))
                 .Column<string>("LastName", c => c.WithLength(50))
                 .Column<string>("EmailAddress", c => c.WithLength(50))
                 );

            ContentDefinitionManager.AlterPartDefinition("CustomerPart", part => part
                .Attachable(false)
                );

            ContentDefinitionManager.AlterTypeDefinition("Customer", type => type
                .WithPart("CustomerPart")
                .WithPart("UserPart")
                );


            // Return the version that this feature will be after this method completes
            return 1;


            
        }

       
    }
}