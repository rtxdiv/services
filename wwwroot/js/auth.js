const authForm = document.querySelector('#authForm')
const keyInp = document.querySelector('#keyInp')
const error = document.querySelector('.error')

authForm.addEventListener('submit', trykey)

async function trykey(event) {
    event.preventDefault()

    if (!keyInp.value) {
        error.textContent = 'Введите ключ'
        return
    }

    const resp = await fetch('/trykey', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            key: document.querySelector('#keyInp').value
        })
    })

    if (resp.redirected) {
        window.location.href = resp.url
    }

    if (!resp.ok) {
        if (resp.status == 400) error.textContent = 'Введите ключ'
        if (resp.status == 404) error.textContent = 'Неверный ключ'
    }
}