import { number } from 'prop-types';
import React, { BaseSyntheticEvent, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router';
import { Button, Dropdown, Form, Message } from 'semantic-ui-react';
import { IStatus } from '../../interfaces/IStatus';
import { ITask } from '../../interfaces/ITask';
import apiService from '../../services/apiService';

interface Props {
    isUpdate: boolean,
}

const TaskCreateOrEdit = ({ isUpdate}: Props) => {

    const [statusList, setStatusList] = useState<IStatus[]>([]);
    const [task, setTask] = useState<ITask>({ name: '' });
    const { id } = useParams();
    let navigate = useNavigate();
    const [errorMessage, setErrorMessage] = useState<string>('') 

    useEffect(() => {
        if (isUpdate) {
            apiService.Tasks.details(id!)
                .then((task) => {
                    setTask(task);
                })
                .catch((err) => {
                    setErrorMessage(err.response.data);
                })
        }

        apiService.Tasks.statuslist()
            .then((response) => {
                setStatusList(response)
            })
            .catch((err) => {
                setErrorMessage(err.response.data);
            })
    }, [])

    const getOptionList = () => {
        var result = [];

        for (const status of statusList) {
            var item = {
                key: status.statusText,
                text: status.statusText,
                value: String(status.statusNumber),
            };

            result.push(item);
        }

        return result;
    }

    const onSubmit = () => {
        if (task.name.trim().length === 0) {
            setTask({ ...task, nameError: "Name cannot be empty" });
            return;
        }

        var payload: ITask = {
            id: id,
            name: task.name,
            priorityValue: task.priorityValue,
            statusText: task.statusText
        } 

        isUpdate ?
            apiService.Tasks.update(payload)
                .then(() => navigate("/"))
                .catch((err) => setErrorMessage(err.response.data))
        :
            apiService.Tasks.create(payload)
                .then(() => navigate("/"))
                .catch((err) => setErrorMessage(err.response.data))
        
    }

    return (
        <div>
            {errorMessage && <Message negative>
                <Message.Header>{errorMessage}</Message.Header>
            </Message>}

            <Form onSubmit={onSubmit}>
                <Form.Field>
                    <label>Name</label>
                    <Form.Input value={task?.name || ''} required={true} placeholder='Task name here' onChange={(e) => setTask({ ...task, name: e.target.value })} error={task.nameError} />
                </Form.Field>
                <Form.Field>
                    <label>Priority</label>
                    <Form.Input value={task?.priorityValue || 0} type="number" min="1" max="5" placeholder='Provide number here' onChange={(e) => setTask({ ...task, priorityValue: Number(e.target.value) })}></Form.Input>
                </Form.Field>
                <Form.Field>
                    <label>Status</label>
                    <Dropdown
                        text={task?.statusText || ''}
                        placeholder='Select status'
                        fluid
                        selection
                        options={getOptionList()}
                        onChange={(e) => setTask({ ...task, statusText: e.currentTarget.firstChild?.textContent })}
                    />
                </Form.Field>
                <Button type='submit'>Submit</Button>
            </Form>
        </div>
    );
}

export default TaskCreateOrEdit;