import React from 'react';
import { NavLink } from 'react-router-dom';
import { Menu } from 'semantic-ui-react';

const NavBar = () => {


    return (
        <Menu>
            <Menu.Item header as={NavLink} to="/" content="Home" />
            <Menu.Item header as={NavLink} to="/create" content="Create" />
        </Menu>
    )
}

export default NavBar;