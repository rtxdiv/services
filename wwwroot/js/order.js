const queryInp = document.querySelector('#queryInp')
const contactInp = document.querySelector('#contactInp')

async function sendForm() {

    const resp = await fetch('/send', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            serviceId: window.location.pathname.split('/').at(-1),
            query: queryInp.value,
            contact: contactInp.value
        })
    })

    if (resp.redirected) {
        window.location.href = resp.url
    }

    if (!resp.ok) {
        if (resp.status == 400) {
            const body = await resp.json()
            return displayErrors(body)
        }
        document.querySelector(`[data-error="All"]`).textContent = 'Ошибка сервера'
    }
}

let errorElems = []
function displayErrors(errors) {
    errorElems.forEach(elem => {
        elem.textContent = ''
    })
    errorElems = []
    Object.entries(errors).forEach(([key, value]) => {
        const elem = document.querySelector(`[data-error="${key}"]`)
        errorElems.push(elem)
        elem.textContent = value[0]
    })
}