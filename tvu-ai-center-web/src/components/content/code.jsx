import { useEffect, useState } from "react";
import { Prism as SyntaxHighlighter } from "react-syntax-highlighter";
import oneDark from "react-syntax-highlighter/dist/esm/styles/prism/one-dark";
import hljs from "highlight.js";

const acceptedLanguages = [
  "python",
  "javascript",
  "java",
  "c",
  "cpp",
  "csharp",
  "go",
  "ruby",
  "php",
  "swift",
  "typescript",
  "html",
  "css",
  "bash",
  "json",
  "xml",
  "sql",
];

const Code = ({ data }) => {
  const [copy, setCopy] = useState(false);
  const [language, setLanguage] = useState("cpp");

  const copyToClipboard = () => {
    if (copy) return;
    navigator.clipboard.writeText(data.code);
    setCopy(true);
    setTimeout(() => {
      setCopy(false);
    }, 3000);
  };

  useEffect(() => {
    const result = hljs.highlightAuto(data.code);
    console.log(result.language);
    if (acceptedLanguages.includes(result.language)) {
      setLanguage(result.language);
    }
  }, []);

  return (
    <div style={{ position: "relative" }}>
      <button
        onClick={copyToClipboard}
        style={{
          position: "absolute",
          top: 10,
          right: 10,
          backgroundColor: "#333",
          color: "white",
          border: "none",
          padding: "5px 10px",
          borderRadius: "5px",
          cursor: "pointer",
          fontSize: "12px",
        }}
      >
        {copy ? "Copied" : "Copy"}
      </button>
      <SyntaxHighlighter language={language} style={oneDark} showLineNumbers>
        {data.code}
      </SyntaxHighlighter>
    </div>
  );
};

export default Code;
