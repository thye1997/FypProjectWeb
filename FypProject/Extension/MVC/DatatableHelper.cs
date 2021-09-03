
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FypProject.Base;
using FypProject.Config;
using FypProject.ViewModel;
//using FypProject.Base;
using Microsoft.AspNetCore.Mvc;

    public static class DatatableHelper
    {


     public static JsonResult DataTableResult<T>(this Controller controller, IDictionary<string, string> dict, List<T> list) 
          
     {
         var draw = dict[SystemData.DatatableRequest.draw];
         var start = dict[SystemData.DatatableRequest.start];
         var length = dict[SystemData.DatatableRequest.length];

         var pageSize = length != null ? Convert.ToInt32(length) : 0;
         var skip = start != null ? Convert.ToInt32(start) : 0;

         Debug.WriteLine($"value of draw {draw} , start {start}, length {length}");

         var recordsTotal = list.Count();
         var data = list.Skip(skip).Take(pageSize).ToList();
         return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data});
     }

    //create function for using view model
    public static JsonResult DataTableResult<VM,T>(this Controller controller, IDictionary<string, string> dict, List<VM> list, bool isViewModel = true)
        where VM : ListViewModel<T>, new()
        where T : class,  new()
    {
        var draw = dict[SystemData.DatatableRequest.draw];
        var start = dict[SystemData.DatatableRequest.start];
        var length = dict[SystemData.DatatableRequest.length];

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        Debug.WriteLine($"value of draw {draw} , start {start}, length {length}");

        var recordsTotal = list.Count();
        var data = list.Skip(skip).Take(pageSize).ToList();
        return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
    }
    public static JsonResult DataTableResult<T>(this Controller controller, IDictionary<string, string> dict, List<T> list, Action<List<object>> datas, bool hasCustomId = false) 
        where T : IBusinessEntity, new()
    {
        var draw = dict[SystemData.DatatableRequest.draw];
        var start = dict[SystemData.DatatableRequest.start];
        var length = dict[SystemData.DatatableRequest.length];

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;
         List<object> data = null;
        Debug.WriteLine($"value of draw {draw} , start {start}, length {length}");
        data = new List<object>();
        datas?.Invoke(data);
        /*if (hasCustomId)
        {
            data = new List<object>();
            var count = 0;
            foreach (var obj in list)
            {
                data.Add(new
                {   
                    customId = count++,
                    data = obj
                });
            }
        }
        else{
        }*/
        //dataList(list);
        //data = (List<object>)(object)list.Skip(skip).Take(pageSize).ToList();

        var recordsTotal = list.Count();
        //var data = list.Skip(skip).Take(pageSize).ToList();
        return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data});
    }
}

