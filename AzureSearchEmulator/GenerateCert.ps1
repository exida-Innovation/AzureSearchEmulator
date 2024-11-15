$params = @{
    "DnsName"            = @("localhost")
    "CertStoreLocation"  = "Cert:LocalMachine\\My"
    "NotAfter"           = (Get-Date).AddMonths(48)
    "KeyAlgorithm"       = "RSA"
    "KeyLength"          = "2048"
}

New-SelfSignedCertificate @params