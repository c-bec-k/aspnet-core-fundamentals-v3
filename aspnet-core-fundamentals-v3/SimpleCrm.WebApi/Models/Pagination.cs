using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Models {
  public class PaginationModel {
    public string next { get; set; }
    public string prev { get; set; }
    public string first { get; set; }
    // public string last { get; set; }
  }
}