import { getTokenWorkaround } from "@/app/actions/auth-actions";

const baseUrl = process.env.URL;

async function get(url: string) {
    const requestOptions = {
        method: 'GET',
        headers: await getHeaders()
    }

    const response = await fetch(baseUrl + url, requestOptions);
    return await handleResponse(response);
}

async function post(url: string, body: {}) {
    const requestOptions = {
        method: 'POST',
        headers: await getHeaders(),
        body: JSON.stringify(body)
    }

    const response = await fetch(baseUrl + url, requestOptions);
    return await handleResponse(response);
}

async function postForm(url: string, formData: FormData) {
    const requestOptions = {
        method: 'POST',
        headers: await getAuthenticationHeader(),
        body: formData
    }

    const response = await fetch(baseUrl + url, requestOptions);
    return await handleResponse(response);
}

async function put(url: string, body: {}) {
    const requestOptions = {
        method: 'PUT',
        headers: await getHeaders(),
        body: JSON.stringify(body)
    }

    const response = await fetch(baseUrl + url, requestOptions);
    return await handleResponse(response);
}

async function putForm(url: string, formData: FormData) {
    const requestOptions = {
        method: 'PUT',
        headers: await getAuthenticationHeader(),
        body: formData
    }

    const response = await fetch(baseUrl + url, requestOptions);
    return await handleResponse(response);
}


async function del(url: string) {
    const requestOptions = {
        method: 'DELETE',
        headers: await getHeaders(),
    }


    const response = await fetch(baseUrl + url, requestOptions);
    return await handleResponse(response);
}

async function getAuthenticationHeader() {
    const token = await getTokenWorkaround();
    const headers = {} as any;
    if (token) {
        headers.Authorization = 'Bearer ' + token.accessToken
    }

    return headers;
}

async function getHeaders() {
    const token = await getTokenWorkaround();
    const headers = { 'Content-type': 'application/json' } as any;

    if (token) {
        headers.Authorization = 'Bearer ' + token.accessToken
    }

    return headers;
}

async function handleResponse(response: Response) {
    const text = await response.text();

    let data
    try {
        data = JSON.parse(text);
    } catch (error) {
        data = text;
    }

    if (response.ok) {
        return data || response.statusText;
    } else {
        const error = {
            status: response.status,
            message: (typeof data === 'object' && data.name) ? data.name : response.statusText
        }

        return { error }
    }
}

export const fetchWrapper = {
    get,
    post,
    put,
    del,
    postForm,
    putForm
}