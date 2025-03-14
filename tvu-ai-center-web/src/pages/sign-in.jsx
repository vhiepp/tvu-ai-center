import React from "react";
import SEO from "../common/seo";
import SignIn from "../components/sign-in";
import Wrapper from "../layout/wrapper";
import EmptyLayout from "@/layout/empty";

const index = () => {
  return (
    <Wrapper>
      <SEO pageTitle={"Softec - Data analytics"} />
      <SignIn />
    </Wrapper>
  );
};

index.Layout = EmptyLayout;

export default index;
