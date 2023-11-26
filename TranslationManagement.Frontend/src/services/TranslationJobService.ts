
export async function getTranslationJobs() {
    const response = await fetch('/api/jobs', {
        method: 'GET',
        headers: {
            Accept: 'application/json',
        }
    });

    const data = await response.json();
    return data;
}

export async function deleteTranslationJob(id: number) {
    await fetch(`/api/jobs/${id}`, {
        method: 'DELETE'
    });
}

export async function addTranslationJob(job: FormData) {
    const response = await fetch('/api/jobs', {
        method: 'POST',
        body: job
    });
    console.log(response);
    const data = await response.json();
    return data;
}
