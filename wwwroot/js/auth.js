async function trykey() {

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
        if (resp.status == 400) console.log('Введите ключ')
        if (resp.status == 404) console.log('Неверный ключ')
    }
}