#az login
# Update for your admin password
#$AdminPassword="ChangeYourAdminPassword1"
 
# Create a resource group.
az group create --name myNewtt2 --location eastus
 
# Create a virtual network.
az network vnet create --resource-group myNewtt2 --name myVvnett2 --subnet-name mySubNettt2
 
# Create a public IP address.
az network public-ip create --resource-group myNewtt2 --name myPublicIP2
 
# Create a network security group.
az network nsg create --resource-group myNewtt2 --name myNetworkSecurityy2
 
# Create a virtual network card and associate with public IP address and NSG.
az network nic create --resource-group myNewtt2 --name myNiccc2 --vnet-name myVvnett2 --subnet mySubnettt2 --network-security-group myNetworkSecurityy2 --public-ip-address myPublicIP2
 
# Create a virtual machine.
az vm create --resource-group myNewtt2 --name NewtVM2 --location eastus --nics myNiccc2 --image win2012R2datacenter --admin-username newtdemo --admin-password Newt!dEmo123
 
# Open port 3389 to allow RDP traffic to host.
az vm open-port --port 3389 --resource-group myNewtt2 --name NewtVM2