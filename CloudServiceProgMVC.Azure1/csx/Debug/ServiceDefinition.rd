<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CloudServiceProgMVC.Azure1" generation="1" functional="0" release="0" Id="fa368c70-cae4-49a5-851a-82ab7fe665d2" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
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
        <aCS name="CloudServiceProgMVC:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/MapCloudServiceProgMVC:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="CloudServiceProgMVC:TableStorageConnection" defaultValue="">
          <maps>
            <mapMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/MapCloudServiceProgMVC:TableStorageConnection" />
          </maps>
        </aCS>
        <aCS name="CloudServiceProgMVCInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/MapCloudServiceProgMVCInstances" />
          </maps>
        </aCS>
        <aCS name="SignupsWorker1:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/MapSignupsWorker1:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="SignupsWorker1:TableStorageConnection" defaultValue="">
          <maps>
            <mapMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/MapSignupsWorker1:TableStorageConnection" />
          </maps>
        </aCS>
        <aCS name="SignupsWorker1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/MapSignupsWorker1Instances" />
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
        <map name="MapCloudServiceProgMVC:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVC/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapCloudServiceProgMVC:TableStorageConnection" kind="Identity">
          <setting>
            <aCSMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVC/TableStorageConnection" />
          </setting>
        </map>
        <map name="MapCloudServiceProgMVCInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVCInstances" />
          </setting>
        </map>
        <map name="MapSignupsWorker1:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/SignupsWorker1/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapSignupsWorker1:TableStorageConnection" kind="Identity">
          <setting>
            <aCSMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/SignupsWorker1/TableStorageConnection" />
          </setting>
        </map>
        <map name="MapSignupsWorker1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/SignupsWorker1Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="CloudServiceProgMVC" generation="1" functional="0" release="0" software="C:\Users\Christian Cordts\Documents\GitHub\CloudServiceProgMVC\CloudServiceProgMVC.Azure1\csx\Debug\roles\CloudServiceProgMVC" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="TableStorageConnection" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;CloudServiceProgMVC&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CloudServiceProgMVC&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;SignupsWorker1&quot; /&gt;&lt;/m&gt;" />
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
        <groupHascomponents>
          <role name="SignupsWorker1" generation="1" functional="0" release="0" software="C:\Users\Christian Cordts\Documents\GitHub\CloudServiceProgMVC\CloudServiceProgMVC.Azure1\csx\Debug\roles\SignupsWorker1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="TableStorageConnection" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;SignupsWorker1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CloudServiceProgMVC&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;SignupsWorker1&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/SignupsWorker1Instances" />
            <sCSPolicyUpdateDomainMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/SignupsWorker1UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/SignupsWorker1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="CloudServiceProgMVCUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="SignupsWorker1UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="CloudServiceProgMVCFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="SignupsWorker1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="CloudServiceProgMVCInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="SignupsWorker1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="952df75d-5558-46b5-ba1c-b1bbef32fd4b" ref="Microsoft.RedDog.Contract\ServiceContract\CloudServiceProgMVC.Azure1Contract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="83065134-6cb4-423e-8ada-e04c30a7b921" ref="Microsoft.RedDog.Contract\Interface\CloudServiceProgMVC:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CloudServiceProgMVC.Azure1/CloudServiceProgMVC.Azure1Group/CloudServiceProgMVC:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>