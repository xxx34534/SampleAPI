using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schroders.Contracts
{
    public class TestModel
    {
        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        public int TestId {get;set;}

        /// <summary>
        /// Gets or sets the test name.
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// Gets or sets the created date of a test.
        /// </summary>
        public DateTime Created {get;set;}

        /// <summary>
        /// Gets or sets the date, when the test was updated.
        /// </summary>
        public DateTime Updated {get;set;}

        /// <summary>
        /// Gets or sets the name of a user, who updated the test.
        /// </summary>
        public string UpdatedBy {get;set;}

        /// <summary>
        /// Gets or sets the name of a user, who created the test.
        /// </summary>
        public string CreatedBy {get;set;}
    }
}
