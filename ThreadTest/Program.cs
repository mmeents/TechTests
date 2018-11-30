using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest{

  class Program {
    

    static void Main(string[] args){
           
      AsyncCounter acMain = new AsyncCounter();

      // possibly wait for count to finish.
      while((acMain.ThreadA.IsBusy || acMain.ThreadB.IsBusy)&&(acMain.iVarB<100)) { } 

      //Console.ReadKey();  uncomment if you want to look at results.
    }

    
  }

  class AsyncCounter {
    public Int32 iVarA = -1;
    public Int32 iVarB = 0;
    
    public BackgroundWorker ThreadA;
    public BackgroundWorker ThreadB;
    public AsyncCounter() {
      ThreadA = new BackgroundWorker();
      ThreadA.WorkerSupportsCancellation = false;
      ThreadA.DoWork += new System.ComponentModel.DoWorkEventHandler(IncVarA);
      ThreadA.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(IncVarAComplete);

      ThreadB = new BackgroundWorker();
      ThreadB.WorkerSupportsCancellation = false;
      ThreadB.DoWork += new System.ComponentModel.DoWorkEventHandler(IncVarB);
      ThreadB.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(IncVarBComplete);

      ThreadA.RunWorkerAsync();
    }
    private void IncVarA(object sender, DoWorkEventArgs e){
      iVarA = iVarA + 2;
    }
    private void IncVarB(object sender, DoWorkEventArgs e){
      iVarB = iVarB + 2;
    }
    private void IncVarAComplete(object sender, RunWorkerCompletedEventArgs e){
      Console.WriteLine("Thread 1: The number is '" + Convert.ToString(iVarA) + "'");
      ThreadB.RunWorkerAsync();
    }
    private void IncVarBComplete(object sender, RunWorkerCompletedEventArgs e){
      Console.WriteLine("Thread 2: The number is '" + Convert.ToString(iVarB) + "'");
      if (iVarB < 100) { 
        ThreadA.RunWorkerAsync();
      }
    }
  }

}
