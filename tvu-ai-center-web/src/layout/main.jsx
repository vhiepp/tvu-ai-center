import React from "react";
import HeaderTwo from "@/layout/headers/header-2";
import FooterTwo from "./footers/footer-2";
import Footer from "./footers/footer";
import FooterThree from "./footers/footer-3";
import FooterFour from "./footers/footer-4";

function MainLayout({ children, white_header = false, style_error = true }) {
  return (
    <>
      <HeaderTwo />
      {children}
      <FooterFour />
    </>
  );
}

export default MainLayout;
