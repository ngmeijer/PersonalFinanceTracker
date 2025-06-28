//Enables the proper modal
document.querySelectorAll('[id$="-budget-button"').forEach(button => {
    button.addEventListener('click', () => {
        const modalType = button.id.split('-')[0];
        const modal = document.querySelector(`.budget-action-modal[data-modal="${modalType}"]`);
        modal.style.display = "flex";
        getPageContent().classList.add('blur');
    });
});

//Closes the current modal
document.querySelectorAll('.cancel-button').forEach(button => {
    button.addEventListener('click', () => {
        const modalType = button.dataset.modal;
        const modal = document.querySelector(`.budget-action-modal[data-modal="${modalType}"]`);
        modal.style.display = "none";
        getPageContent().classList.remove('blur');

        const errorText = modal.querySelector('.error-content');
        if (errorText) {
            errorText.innerHTML = "";
            errorText.style.display = "none";
        }
    });
});

document.querySelectorAll('.confirm-button').forEach(button => {
    button.addEventListener('click', () => {
        const modalType = button.dataset.modal;

        switch (modalType) {
            case 'add':
                handleAddBudget();
                break;
            case 'remove':
                handleRemoveBudget();
                break;
            case 'change':
                handleChangeBudget();
                break;
        }
    })
});

const addBudgetModal = document.querySelector(`.budget-action-modal[data-modal="add"]`);
const errorText = addBudgetModal.querySelector('.error-content');

function handleAddBudget() {
    var givenName = $('#name').val();
    var givenAmount = parseInt($('#amount').val(), 10);
    var givenTimeframe = parseInt($('#timeframe').val(), 10);
    var requiredData = {
        Name: givenName,
        Amount: givenAmount,
        Timeframe: givenTimeframe,
    };

    $.ajax({
        url: addBudgetUrl,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requiredData),
        success: function (response) {
            console.log('Success:', response)
            addBudgetModal.style.display = "none";
            getPageContent().classList.remove('blur');

            if (errorText) {
                errorText.style.display = 'none';
                errorText.innerHTML = "";
            }
        },
        error: function (response) {
            console.log('Failure:', response, " - provided data:", requiredData, " - stringified data:", JSON.stringify(requiredData));
        }
    });
}