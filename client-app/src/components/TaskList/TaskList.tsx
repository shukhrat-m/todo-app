import React, { useEffect, useState } from 'react';
import { Segment, Table } from 'semantic-ui-react';
import { ITask } from '../../interfaces/ITask';
import apiService from '../../services/apiService';
import TaskDetails from '../Task/TaskDetails';
import "./TaskList.css";

const TaskList = () => {

    const [tasks, setTasks] = useState<ITask[]>([]);

    useEffect(() => {
        apiService.Tasks.list().then((response) => {
            setTasks(response);
        })
    }, []);

    return (
        <div className="task-list">
            <Table celled compact definition>
                <Table.Header fullWidth>
                    <Table.Row>
                        <Table.HeaderCell>Name</Table.HeaderCell>
                        <Table.HeaderCell>Status</Table.HeaderCell>
                        <Table.HeaderCell>Priority</Table.HeaderCell>
                        <Table.HeaderCell>Manage</Table.HeaderCell>
                    </Table.Row>
                </Table.Header>
                <Table.Body>
                    {tasks.length ?
                        tasks.map((task) => (
                            <TaskDetails key={task.id} name={task.name} id={task.id} priorityValue={task.priorityValue} status={task.status} statusText={task.statusText} />
                        )) : <Table.Row><Table.Cell singleLine={true}> There is nothing for now</Table.Cell></Table.Row>}
                </Table.Body>
            </Table>
        </div>
    );
}

export default TaskList;