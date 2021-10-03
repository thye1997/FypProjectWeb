
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FypProject.Base;
using FypProject.Config;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Mvc;

    public static class DatatableHelper
    {

    //for normal model
    public static JsonResult DataTableResult<T>(this Controller controller, IDictionary<string, string> dict, IQueryable<T> list, Func<T, object> orderBy = null)
        where T: class, IBusinessEntity, new()
    {
        var draw = dict[SystemData.DatatableRequest.draw];
        var start = dict[SystemData.DatatableRequest.start];
        var length = dict[SystemData.DatatableRequest.length];

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        //Debug.WriteLine($"value of draw {draw} , start {start}, length {length}");

        var recordsTotal = list.Count();
            if(orderBy == null)
        {
            orderBy = c => c.Id;
        }
            var data = list.Skip(skip).Take(pageSize).ToList().OrderBy(orderBy).ToList();

        return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
    }

    //for  view model
    public static JsonResult DataTableResult<VM, T>(this Controller controller, IDictionary<string, string> dict, IQueryable<VM> list, bool isViewModel = true, Func<VM, object> orderBy = null) // use orderBy in Enumerable before changing date type in db
        where VM : ListViewModel<T>, new()
        where T : class, new()
    {
        var draw = dict[SystemData.DatatableRequest.draw];
        var start = dict[SystemData.DatatableRequest.start];
        var length = dict[SystemData.DatatableRequest.length];

        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        var skip = start != null ? Convert.ToInt32(start) : 0;

        //Debug.WriteLine($"value of draw {draw} , start {start}, length {length}");
        var data = (List <VM>)null;

        if (orderBy == null)
        {
            data = list.Skip(skip).Take(pageSize).ToList();
        }
        else
        {
            data = list.Skip(skip).Take(pageSize).AsEnumerable().OrderBy(orderBy).ToList();

        }

        var recordsTotal = list.Count();
        return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
    }

}

