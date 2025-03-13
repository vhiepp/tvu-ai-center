import React from "react";
import FooterTwo from "./footers/footer-2";
import HeaderTwo from "@/layout/headers/header-2";

function MainLayout({ children, white_header = false, style_error = true }) {
  return (
    <>
      <HeaderTwo />
      {children}
      <FooterTwo />
    </>
  );
}

export default MainLayout;