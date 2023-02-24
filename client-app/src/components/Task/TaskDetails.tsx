import React from 'react';
import { NavLink } from 'react-router-dom';
import { Button, Table } from 'semantic-ui-react';
import { ITask } from '../../interfaces/ITask';

function TaskDetails({ id, name, priorityValue, statusText }: ITask) {
    const [open, setOpen] = React.useState(false)

    return (
        <Table.Row>
            <Table.Cell>{name}</Table.Cell>
            <Table.Cell>{statusText}</Table.Cell>
            <Table.Cell>{priorityValue}</Table.Cell>
            <Table.Cell width="2">
                <Button.Group>
                    <Button as={NavLink} to={`/update/${id}`}>Update</Button>
                    <Button.Or />
                    <Button as={NavLink} to={`/delete/${id}`} color="red">Delete</Button>
                </Button.Group>
            </Table.Cell>
        </Table.Row>
    );
}

export default TaskDetails;