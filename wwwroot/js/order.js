const queryInp = document.querySelector('#queryInp')
const contactInp = document.querySelector('#contactInp')
const queryLength = document.querySelector('#queryLength')

const queryMin = 100
const queryMax = 2000

async function sendForm() {
    let error = false
    if (queryInp.value.length < queryMin || queryInp.value.length > queryMax) error = true
    if (queryInp.value.length == 0) {
        queryInp.classList.add('input-error')
        error = true
    }
    if (contactInp.value.length == 0) {
        contactInp.classList.add('input-error')
        error = true
    }

    if (error) return

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

queryInp.addEventListener('input', function() {
    const length = queryInp.value.length
    if (length == 0) {
        return queryLength.textContent = ''
    }

    queryLength.textContent = `${length}/2000`

    if (length < queryMin) {
        queryLength.textContent = `${length}/100`
        queryLength.classList.add('length-error')

    } else if (length > queryMax) {
        queryLength.classList.add('length-error')
    } else {
        queryLength.classList.remove('length-error')
    }
})

queryInp.addEventListener('change', function() {
    queryInp.classList.remove('input-error')
})
contactInp.addEventListener('change', function() {
    contactInp.classList.remove('input-error')
})