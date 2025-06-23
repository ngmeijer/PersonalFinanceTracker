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