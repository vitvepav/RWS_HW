import { Translator } from "../models/Translator";


export async function getTranslators() {
    const response = await fetch('/api/translators', {
        method: 'GET',
        headers: {
            Accept: 'application/json',
        }
    });

    const data = await response.json();
    return data;
}

export async function deleteTranslator(id: number) {
    await fetch(`/api/translators/${id}`, {
        method: 'DELETE'
    });
}

export async function addTranslator(translator: Translator) {
    const response = await fetch('/api/translators', {
        method: 'POST',
        headers: {
            "Content-Type": 'application/json',
        },
        body: JSON.stringify(translator)
    });
    console.log(response);
    const data = await response.json();
    return data;
}
