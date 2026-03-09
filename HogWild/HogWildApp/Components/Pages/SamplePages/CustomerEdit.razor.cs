using HogWildSystem.BLL;
using HogWildSystem.ViewModels;
using Microsoft.AspNetCore.Components;

namespace HogWildApp.Components.Pages.SamplePages
{
    public partial class CustomerEdit
    {
        #region Fields
        //  feedback message
        private string feedbackMessage = string.Empty;

        //  error message
        private string errorMessage = string.Empty;

        //  has feedback
        private bool hasFeedback => !string.IsNullOrWhiteSpace(feedbackMessage);

        //  has error
        private bool hasError => !string.IsNullOrWhiteSpace(errorMessage) || errorDetails.Count > 0;

        //  error details
        private List<string> errorDetails = new();

        #region

        #region Properties
        //  customer service
        [Inject]
        protected CustomerService? CustomerService { get; set; } = null;

        //  Customer ID used to create or edit a customer
        [Parameter]
        public int CustomerID { get; set; } = 0;

        #endregion
    }
}
