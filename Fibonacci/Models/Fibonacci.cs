using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Fibonacci.Commands;

namespace Fibonacci.Models
{
    public class Fibonacci : INotifyPropertyChanged
    {
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand PauseCommand { get; }

        public ICommand StartCommand2 { get; }
        public ICommand StopCommand2 { get; }
        public ICommand PauseCommand2 { get; }

        public int txtnum { get; set; }

        private string _value;

        public string Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        private Thread _thread;
        private CancellationTokenSource _tokenSource;

        public Fibonacci()
        {
            StartCommand = new DelegateCommand((p) =>
                {
                    _tokenSource = new CancellationTokenSource();
                    _thread = new Thread(Worker) { IsBackground = true };
                    _thread.Start(_tokenSource.Token);
                },
                p => _thread == null);

            PauseCommand = new DelegateCommand(p =>
            {
                _tokenSource.Cancel();
                _tokenSource = null;
                _thread = null;
               
            }, p => _thread != null);

            StopCommand = new DelegateCommand(p =>
            {
                _tokenSource.Cancel();
                _tokenSource = null;
                _thread = null;
                Value = string.Empty;
                a = 0;
                b = 1;
                total = 0;
                j = 1;
            }, p => _thread != null);

            StartCommand2 = new DelegateCommand((p) =>
                {
                    _tokenSource = new CancellationTokenSource();
                    _thread = new Thread(Worker2) { IsBackground = true };
                    _thread.Start(_tokenSource.Token);
                },
                p => _thread == null);

            PauseCommand2 = new DelegateCommand(p =>
            {
                _tokenSource.Cancel();
                _tokenSource = null;
                _thread = null;
            }, p => _thread != null);

            StopCommand2 = new DelegateCommand(p =>
            {
                _tokenSource.Cancel();
                _tokenSource = null;
                _thread = null;
                Value2 = string.Empty;
                _min = 2;
                _max = 200000000;
                

            }, p => _thread != null);

            _max = 200000000;
            _min = 2;
        }

        private long a = 0, b = 1, total = 0, j = 1;

       
        private void Worker(object state)
        {
            _flagCount = false;
            
            var token = (CancellationToken)state;
            while (!token.IsCancellationRequested)
            {
                if (j < txtnum)
                {
                    for (long i = j; i <= txtnum; i++)
                    {
                        total = a + b;
                        a = b;
                        b = total;
                        Value += a + " ";
                        j++;
                        Thread.Sleep(1000);
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                }

                if (j > txtnum && !_flagCount)
                {
                    Value += "\nРасчет окончен";
                    _flagCount = true;
                }
               
                Thread.Sleep(1000);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        private string _value2;

        public string Value2
        {
            get => _value2;
            set
            {
                if (value == _value2) return;
                _value2 = value;
                OnPropertyChanged();
            }
        }

        private int _min;

        public int Min
        {
            get => _min;
            set
            {
                if (value == _min) return;
                _min = value;
               OnPropertyChanged();
            }
        }

        private int _max;

        public int Max
        {
            get => _max;
            set
            {
                if (value == _max) return;
                _max = value;
               OnPropertyChanged();
            }
        }

        public bool IsPrime(int x)
        {
            for (int i = 2; i < x; i++)
                if ((x % i) == 0)
                    return false;
            if (x <= 1)
                return false;
            return true;
        }

        private int _count;
        private bool _flagCount;
        private void Worker2(object state)
        {
             _flagCount = false;
            _count = _min;
            var token = (CancellationToken)state;
            while (!token.IsCancellationRequested)
            {
                for (int i = _min; i <= _max; i++)
                {
                    _count++;

                    if (IsPrime(i))
                    {
                        Value2 += i + " ";
                        _min = i + 1;
                        
                    }

                    if (_count > _max && !_flagCount)
                    {
                        Value2 += "\nРасчет окончен";
                        _flagCount = true;
                    }

                    Thread.Sleep(500);
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
