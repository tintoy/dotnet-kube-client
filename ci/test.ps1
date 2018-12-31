$testsDir = Join-Path $PSScriptRoot 'test'
$testProjects = Get-ChildItem $testsDir -Recurse -File -Filter '*.Tests.csproj'

$failingProjects = @()
ForEach ($testProject In $testProjects) {
	dotnet test -l trx --no-build $testProject.FullName

	If ($LASTEXITCODE) {
		$failingProjects += $testProject.Name.Replace($testProject.Extension, '')
	}
}

If ($failingProjects) {
	Throw "The following projects have one or more failing tests: [$([string]::Join(', ', $failingProjects))]."
}
