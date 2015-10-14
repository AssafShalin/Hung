using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HLL.Models
{
    class Models
    {
        Dictionary<string, Model> models = new Dictionary<string,Model>();
        public Models()
        {

        }
        public Model GetModel(string model)
        {
            if(models.ContainsKey(model))
            {
                return models[model];
            }
            try 
            {
                var instance = Assembly.GetExecutingAssembly().CreateInstance(model, true);
                if (instance is Model) return instance as Model;
                else throw new Exception(model + " is not a model object");

            }
            catch
            {
                throw new Exception("Model " + model + " not found");
            }
            
            
            
        }

    }
}
