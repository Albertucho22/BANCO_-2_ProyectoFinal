using System;

namespace EmployeesDashboard.Data {
  public class API {
    private static string API_URL {
      get {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() == "production")
          return "https://core-2.azurewebsites.net/api/";

        return "https://localhost:6001/api/";
      }
    }

    public static string URL() {
      return API_URL;
    }

    public static string URL(string endpoint) {
      return API_URL + endpoint;
    }
  }
}