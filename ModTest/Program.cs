using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModTest { 
  /// <summary>
  /// Submission for the Optimized Math problem.  Problem displays the use of Mod operator %
  /// </summary>
  class Program{
    static void Main(string[] args) {
      for (Int32 i = 1; i<=100; i++) {
        if (i%6 == 0){
          Console.WriteLine("The number '"+Convert.ToString(i)+"' is divisible by two and three.");
        } else if (i%3==0) {
          Console.WriteLine("The number '" + Convert.ToString(i) + "' is divisible by three.");
        } else if (i%2==0){
          Console.WriteLine("The number '" + Convert.ToString(i) + "' is even.");
        } else {
          Console.WriteLine("The number '" + Convert.ToString(i) + "' is odd.");
        }
      }     
    }
  }

}
