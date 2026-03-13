using HogWildSystem.BLL;
using HogWildSystem.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MudBlazor;
using static MudBlazor.Icons;

namespace HogWildApp.Components.Pages.SamplePages
{
    public partial class CustomerEdit
    {
        #region Fields
        //  customer
        private CustomerEditView customer = new CustomerEditView();
        //provinces
        private List<LookupView> provinces = new();
        //countries
        private List<LookupView> countries = new();
        //status lookup
        private List<LookupView> statusLookup = new();
        //  mudform control
        private MudForm customerForm = new MudForm();

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

        #endregion

        #region Properties
        //  customer service
        [Inject]
        protected CustomerService? CustomerService { get; set; } = null;

        //  category/lookup service
        [Inject]
        protected CategoryLookupService? CategoryLookupService { get; set; } = null;

        //  Customer ID used to create or edit a customer
        [Parameter]
        public int CustomerID { get; set; } = 0;

        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
                //  clear previous error details and meesages
                errorDetails.Clear();
                errorMessage = string.Empty;
                feedbackMessage = string.Empty;

                //  check to see if we are naviagating using a valid customer CustomerID
                //      or are we going to create a new user
                if (CustomerID > 0)
                {
                    var result = CustomerService.GetCustomer(CustomerID);
                    if (result.IsSuccess)
                    {
                        customer = result.Value;
                    }
                    else
                    {
                        errorDetails = BlazorHelperClass.GetErrorMessages(result.Errors.ToList());
                    }
                }
                else
                {
                    customer = new CustomerEditView();
                }
                // lookups
                provinces = CategoryLookupService.GetLookupView("Province").Value;
                countries = CategoryLookupService.GetLookupView("Country").Value;
                statusLookup = CategoryLookupService.GetLookupView("Customer Status").Value;

                //  update UI based on the data has changed
                StateHasChanged();
            }
            catch (Exception ex)
            {
                //  capture any exception message for display
                errorMessage = ex.Message;
            }
        }

        //  save the customer
        private void Save()
        {
            //	clear previous error details and messages
            errorDetails.Clear();
            errorMessage = string.Empty;
            feedbackMessage = string.Empty;

            //	wrap the service call in a try/catch to handle unexpected exceptions
            try
            {
                var result = CustomerService.AddEditCustomer(customer);
                if (result.IsSuccess)
                {
                    customer = result.Value;
                    feedbackMessage = "Data was successfully saved";
                }
                else
                {
                    errorDetails = BlazorHelperClass.GetErrorMessages(result.Errors.ToList());
                }
            }
            catch (Exception ex)
            {
                // capture any exception message for display
                errorMessage = ex.Message;
            }
        }

        //  Cancels/close 
        private void Cancel()
        {

        }

        #endregion
    }
}
