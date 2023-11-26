import { useEffect, useState } from "react";
import { addTranslationJob, deleteTranslationJob, getTranslationJobs } from "../../services/TranslationJobService";
import { PostTranslationJob, TranslationJob } from "../../models/TranslationJob";



function TranslationJobList() {
    const [transJobs, setTransJobs] = useState<TranslationJob[]>([]);
    const [newTransJob, setNewTransJob] = useState<PostTranslationJob>({customerName: "", originalContent: "", fileToTranslate: undefined});

    useEffect(() => {
        getTranslationJobs()
        .then((data) => {
            setTransJobs(data);
        })
        .catch(err => {
            // handle error
            setTransJobs([]);
        });
    }, []);

    const onDeleteJob = (id: number) => {
        deleteTranslationJob(id)
        .then(() => {
            setTransJobs(transJobs.filter((transJob) => transJob.id !== id));
        })
        .catch(err => {
            // handle error
            setTransJobs([]);
        });
    }

    function onAddTranslationJob() {
        console.log(newTransJob);
        const formData = new FormData();

        formData.append("customerName", newTransJob.customerName);

        if (newTransJob.fileToTranslate) {
            formData.append(
                "fileToTranslate",
                newTransJob.fileToTranslate,
                newTransJob.fileToTranslate.name
            );
        }

        if (newTransJob.originalContent) {
            formData.append("originalContent", newTransJob.originalContent);
        }

        addTranslationJob(formData)
        .then((data) => {
            console.log(data);
            setTransJobs([...transJobs, data]);
            setNewTransJob({customerName: "", originalContent: "", fileToTranslate: undefined});
        })
        .catch(err => {
            // handle error
            setTransJobs([]);
        });
        return "";
    }

    const onFormInputChange = (e) => {
        setNewTransJob({ ...newTransJob, [e.target.name]: e.target.value });
    };

    const onFileChange = (e) => {
        if (e.target.files) {
            setNewTransJob({ ...newTransJob, fileToTranslate: e.target.files[0] });
        }
    };

    return (
        <>
            <div>
                <h1>Translation Job List</h1>
            </div>
            <ul>
                {transJobs.map((transJob) => (<li key={transJob.id}>{transJob.id} - {transJob.customerName} - {transJob.originalContent} <button onClick={() => onDeleteJob(transJob.id)}>X</button></li>))}
            </ul>

            <label>
            Name:
            <input type="text" name="customerName" value={ newTransJob.customerName } onChange={(e) => onFormInputChange(e)} />
        </label>
        <label>
            Text to translate:
            <input type="text" name="originalContent" value={ newTransJob.originalContent } onChange={(e) => onFormInputChange(e)}/>
        </label>
        <label>
            File to translate:
            <input type="file" name="fileToTranslate" onChange={(e) => onFileChange(e)}/>
        </label>

            <button onClick={onAddTranslationJob}>Add</button>
        </>
    );
}

export default TranslationJobList