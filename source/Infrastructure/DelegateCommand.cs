//
// Copyright (c) 2021 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/Capreze/blob/master/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Karamem0.Capreze.Infrastructure
{

    public class DelegateCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private Action onExecute;

        private Func<bool> onCanExecute;

        public DelegateCommand(Action onExecute)
        {
            this.onExecute = onExecute;
            this.onCanExecute = delegate { return true; };
        }

        public DelegateCommand(Action onExecute, Func<bool> onCanExecute)
        {
            this.onExecute = onExecute;
            this.onCanExecute = onCanExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            this.OnCanExecuteChanged(new EventArgs());
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }

        void ICommand.Execute(object parameter)
        {
            if (this.onExecute != null)
            {
                this.onExecute.Invoke();
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (this.onCanExecute != null)
            {
                return this.onCanExecute.Invoke();
            }
            return false;
        }

    }

}
