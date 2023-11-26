import { useEffect, useState } from "react";
import { addTranslator, deleteTranslator, getTranslators } from "../../services/TranslatorService";
import { Translator } from "../../models/Translator";



function TranslatorList() {
    const [translators, setTranslators] = useState<Translator[]>([]);
    const [newTransaltor, setNewTranslator] = useState<Translator>({name: "", hourlyRate: 0, creditCardNumber: "", id: 0, status: "Applicant"});

    useEffect(() => {
        getTranslators()
        .then((data) => {
            setTranslators(data);
        })
        .catch(err => {
            // handle error
            setTranslators([]);
        });
    }, []);

    const onDeleteTranslator = (id: number) => {
        deleteTranslator(id)
        .then(() => {
            setTranslators(translators.filter((curTranslator) => curTranslator.id !== id));
        })
        .catch(err => {
            // handle error
            setTranslators([]);
        });
    }

    function onAddTranslator() {
        console.log(newTransaltor);
        addTranslator(newTransaltor)
        .then((data) => {
            console.log(data);
            setTranslators([...translators, data]);
            setNewTranslator({name: "", hourlyRate: 0, creditCardNumber: "", id: 0, status: "Applicant"});
        })
        .catch(err => {
            // handle error
            setTranslators([]);
        });
        return "";
    }

    const onFormInputChange = (e) => {
        setNewTranslator({ ...newTransaltor, [e.target.name]: e.target.value });
    };

    return (
        <>
        <div>
        <h1>Translator List</h1>
        </div>
        <ul>
            {translators.map((curTranslator) => (<li key={curTranslator.id}>{curTranslator.id} - {curTranslator.name} - {curTranslator.status} <button onClick={() => onDeleteTranslator(curTranslator.id)}>X</button></li>))}
        </ul>

        <label>
            Name:
            <input type="text" name="name" value={ newTransaltor.name } onChange={(e) => onFormInputChange(e)} />
        </label>
        <label>
            Rate:
            <input type="number" name="hourlyRate" value={ newTransaltor.hourlyRate } onChange={(e) => onFormInputChange(e)}/>
        </label>
        <label>
            Credit Card Number:
            <input type="text" name="creditCardNumber" value={ newTransaltor.creditCardNumber } onChange={(e) => onFormInputChange(e)}/>
        </label>

        <button onClick={onAddTranslator}>Add</button>
    </>
    );
}

export default TranslatorList