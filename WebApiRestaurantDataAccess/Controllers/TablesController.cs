using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiRestaurantDataAccess.Controllers
{
    public class TablesController : ApiController
    {
        private IDataAccessSubCategory<ITableModel> tableData = Factory.InstanceTableDataAccess();

        // GET: api/Tables
        public List<ITableModel> Get()
        {
            return tableData.GetAll();
        }

        // GET: api/Tables/5
        public ITableModel Get(int id)
        {
            if (tableData.GetAll().Exists(x => x.ID == id))
            {
                return tableData.FindById(id);
            }
            else
            {
                return null;
            }
        }

        // POST: api/Tables
        public void Post([FromBody]TableModel table)
        {
            if (ModelState.IsValid)
            {
                if (tableData.CheckIfAlreadyExist(table.NumberOfTable.ToString()) == false)
                {
                    ITableModel model = table;
                    tableData.Create(model);
                }
            }
        }

        // PUT: api/Tables/5
        public void Put([FromBody]TableModel table)
        {
            if (ModelState.IsValid)
            {
                ITableModel model = table;
                tableData.Update(model);
            }
        }

        // DELETE: api/Tables/5
        public void Delete(int id)
        {
            tableData.Delete(id);
        }
    }
}
