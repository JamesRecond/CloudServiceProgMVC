<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CloudServiceProgMVC.Azure1" generation="1" functional="0" release="0" Id="0647a3f6-332a-49d7-8553-e17535b6458d" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CloudServiceProgMVC.Azure1Group" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="CloudServiceProgMVC:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/LB:CloudServiceProgMVC:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="CloudServiceProgMVCInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/MapCloudServiceProgMVCInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:CloudServiceProgMVC:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVC/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCloudServiceProgMVCInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVCInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="CloudServiceProgMVC" generation="1" functional="0" release="0" software="C:\Users\Christian Cordts\Documents\Visual Studio 2013\Projects\CloudServiceProgMVC\CloudServiceProgMVC.Azure1\csx\Debug\roles\CloudServiceProgMVC" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;CloudServiceProgMVC&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CloudServiceProgMVC&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVCInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVCUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVCFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="CloudServiceProgMVCUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="CloudServiceProgMVCFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="CloudServiceProgMVCInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="e570e786-cf24-42f4-878d-1c6f6aa33dd1" ref="Microsoft.RedDog.Contract\ServiceContract\CloudServiceProgMVC.Azure1Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="9802a3f5-0239-49fb-9790-49ed1372164c" ref="Microsoft.RedDog.Contract\Interface\CloudServiceProgMVC:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVC:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>