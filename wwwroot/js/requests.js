Array.from(document.querySelectorAll('.card')).forEach(card => {
    const dateElem = card.querySelector('.date')
    const date = new Date(dateElem.textContent + 'Z')
    dateElem.textContent = `${String(date.getDate()).padStart(2, '0')}/${String(date.getMonth() + 1).padStart(2, '0')}/${date.getFullYear()}`
})