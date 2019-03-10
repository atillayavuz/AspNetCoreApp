import React from "react";
import { MDBContainer} from 'mdbreact';
import Aux from "../../hoc/hoc";
import Navbar from './Navbar';

const layout = props => (
    <Aux>
        <Navbar />
        <main>
            <MDBContainer className="text-center my-5">
                {props.children}
            </MDBContainer>
        </main>
    </Aux>
);
export default layout;
